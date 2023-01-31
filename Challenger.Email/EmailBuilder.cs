using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Challenger.Email
{
    public abstract class EmailBuilder
    {
        private static bool _isConfigured = false;
        private static readonly Dictionary<string, string> _templates = new Dictionary<string, string>();
        private static readonly Regex reg = new Regex(@"{{[A-Za-z0-9]+}}", RegexOptions.Compiled);

        public async Task Configure()
        {
            if (_isConfigured)
            {
                return;
            }

            var asm = this.GetType().Assembly;            
            var embeded = asm.GetManifestResourceNames().Where(x => x.EndsWith(".html"));

            foreach (var e in embeded)
            {
                using (Stream stream = asm.GetManifestResourceStream(e))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string result = await reader.ReadToEndAsync();
                    string key = e.Replace(asm.GetName().Name + ".", "");
                    if (!_templates.ContainsKey(key))
                    {
                        _templates.Add(key, result);
                    }
                }
            }

            _isConfigured = true;
        }

        public string BuildEmailMessage(string emailTemplate, Dictionary<string, object> substitutes)
        {
            var template = _templates[emailTemplate];
            var values = reg.Matches(template).Select(x => x.Value).Distinct().ToArray();

            var res = template;
            for (int i = 0; i < substitutes.Count; i++)
            {
                res = res.Replace(values[i], substitutes[values[i]].ToString());
            }

            return res;
        }

        /// <summary>
        /// Sets the email subject based on user culture and email format
        /// </summary>
        /// <param name="emailType"></param>
        /// <returns></returns>
        public abstract string BuildEmailSubject(string emailType);        
    }
}
