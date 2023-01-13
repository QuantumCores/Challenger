using Challenger.Domain.FormulaParser.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Challenger.Domain.FormulaParser.Models
{
    public class Functions
    {
        public static Dictionary<string, MethodInfo> All { get; } = GetAllFunctions();

        private static Dictionary<string, MethodInfo> GetAllFunctions()
        {
            var result = new Dictionary<string, MethodInfo>();
            result.Add(FunctionTypes.Sin.ToString().ToLower(), typeof(Math).GetMethod("Sin", new[] { typeof(double) }));
            result.Add(FunctionTypes.Cos.ToString().ToLower(), typeof(Math).GetMethod("Cos", new[] { typeof(double) }));
            result.Add(FunctionTypes.Tan.ToString().ToLower(), typeof(Math).GetMethod("Tan", new[] { typeof(double) }));
            result.Add(FunctionTypes.Cot.ToString().ToLower(), typeof(Math).GetMethod("Cot", new[] { typeof(double) }));
            result.Add(FunctionTypes.Log.ToString().ToLower(), typeof(Math).GetMethod("Log", new[] { typeof(double) }));
            result.Add(FunctionTypes.Log10.ToString().ToLower(), typeof(Math).GetMethod("Log10", new[] { typeof(double) }));
            result.Add(FunctionTypes.ASin.ToString().ToLower(), typeof(Math).GetMethod("ASin", new[] { typeof(double) }));
            result.Add(FunctionTypes.ACos.ToString().ToLower(), typeof(Math).GetMethod("ACos", new[] { typeof(double) }));
            result.Add(FunctionTypes.ATan.ToString().ToLower(), typeof(Math).GetMethod("ATan", new[] { typeof(double) }));
            result.Add(FunctionTypes.ACot.ToString().ToLower(), typeof(Math).GetMethod("ACot", new[] { typeof(double) }));

            return result;
        }

        public static Expression Get(string function, Expression arg)
        {
            return Expression.Call(All[function.ToLower()], arg);
        }

        public static Expression Get(string function, Expression arg1, Expression arg2)
        {
            return Expression.Call(All[function.ToLower()], arg1, arg2);
        }

        public static Expression Get(string function, Expression arg1, Expression arg2, Expression arg3)
        {
            return Expression.Call(All[function.ToLower()], arg1, arg2, arg3);
        }
    }
}
