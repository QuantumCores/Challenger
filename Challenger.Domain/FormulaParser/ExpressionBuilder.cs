using Challenger.Domain.FormulaParser.Contracts;
using Challenger.Domain.FormulaParser.Exceptions;
using Challenger.Domain.FormulaParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Challenger.Domain.FormulaParser
{
    public static class ExpressionBuilder
    {
        public static Expression<Func<T, T[], double>> Build<T>(List<ISymbol> RPNStack)
        {
            ParameterExpression arg = Expression.Parameter(typeof(T), "arg");
            ParameterExpression argT = Expression.Parameter(typeof(T[]), "argT");

            List<Expression> stack = new List<Expression>();
            var i = 0;
            try
            {
                for (i = 0; i < RPNStack.Count; i++)
                {
                    var s = RPNStack[i];
                    if (s.Type != SymbolTypes.Number)
                    {
                        if (s.Type == SymbolTypes.BinaryOperator)
                        {
                            var tmp = Expression.MakeBinary(Operators.Get(s.Value).ExpressionType, stack[stack.Count - 2], stack[stack.Count - 1]);
                            stack.RemoveAt(stack.Count - 1);
                            stack.RemoveAt(stack.Count - 1);
                            stack.Add(tmp);
                        }
                        else if (s.Type == SymbolTypes.UnaryOperator)
                        {
                            if (s.Value == OperatorTypes.Sign.ToString())
                            {
                                var tmp = Expression.Multiply(Expression.Constant(-1), stack[stack.Count - 1]);
                                stack.RemoveAt(stack.Count - 1);
                                stack.Add(tmp);
                            }
                        }
                        else if (s.Type == SymbolTypes.Function)
                        {
                            if (s.Value == "if")
                            {
                                var tmp = Expression.Condition(stack[^3], stack[^2], stack[^1]);
                                stack.RemoveAt(stack.Count - 1);
                                stack.RemoveAt(stack.Count - 1);
                                stack.RemoveAt(stack.Count - 1);
                                stack.Add(tmp);
                            }
                            else
                            {
                                var tmp = Functions.Get(s.Value, stack[^1]);
                                stack.RemoveAt(stack.Count - 1);
                                stack.Add(tmp);
                            }
                        }
                        else if (s.Type == SymbolTypes.Variable)
                        {
                            var props = s.Value.Split('.');
                            props.Last();
                            Expression tmp = null;
                            foreach (var propName in props)
                            {
                                if (propName.Contains('['))
                                {
                                    if (Array.IndexOf(props, propName) == 0)
                                    {
                                        tmp = GetArrayValue<T>(argT, propName.FirstToUpper());
                                    }
                                    else
                                    {
                                        tmp = GetArrayValue<T>(tmp, propName.FirstToUpper());
                                    }
                                }
                                else
                                {
                                    if (Array.IndexOf(props, propName) == 0)
                                    {
                                        tmp = GetProperty<T>(arg, propName.FirstToUpper());
                                    }
                                    else
                                    {
                                        tmp = GetProperty<T>(tmp, propName.FirstToUpper());
                                    }
                                }
                            }

                            stack.Add(tmp);
                        }
                    }
                    else
                    {
                        stack.Add(Expression.Constant(double.Parse(s.Value), typeof(double)));
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = "I couldn't undertand what you mean by '" + RPNStack[i].Value + "' before " + string.Join("", stack);
                throw new WrongSyntaxException(msg, ex);
            }

            return Expression.Lambda<Func<T, T[], double>>(stack[0], new[] { arg, argT });
        }

        private static Expression GetProperty<T>(Expression arg, string propName)
        {
            var tmp = Expression.Property(arg, propName);
            if (IsNullable<T>(propName))
            {
                tmp = Expression.PropertyOrField(tmp, "Value");
            }
            return tmp;
        }

        private static Expression GetArrayValue<T>(Expression array, string value)
        {
            var indexString = value.Substring(value.IndexOf('[') + 1, value.IndexOf(']') - value.IndexOf('[') - 1);
            
            if (indexString == "n")
            {
                return Expression.Call(typeof(Enumerable), nameof(Enumerable.Last), new Type[] { typeof(T) }, array);
            }

            return Expression.ArrayIndex(array, Expression.Constant(int.Parse(indexString) - 1));
        }

        private static bool IsNullable<T>(string propName)
        {
            return Nullable.GetUnderlyingType(typeof(T).GetProperty(propName).PropertyType) != null;
        }

        public static string FirstToUpper(this string text)
        {
            return text[0].ToString().ToUpper() + text.Substring(1, text.Length - 1);
        }
    }
}
