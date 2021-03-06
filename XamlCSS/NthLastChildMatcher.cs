﻿using XamlCSS.CssParsing;
using XamlCSS.Dom;

namespace XamlCSS
{
    public class NthLastChildMatcher : NthMatcherBase
    {
        public NthLastChildMatcher(CssNodeType type, string text) : base(type, text)
        {
            Text = text.Substring(11).Replace(")", "");
        }

        protected override string GetParameterExpression(string expression)
        {
            if (expression.Length >= 16)
            {
                return expression.Substring(16).Replace(")", "");
            }

            return null;
        }

        public override MatchResult Match<TDependencyObject, TDependencyProperty>(StyleSheet styleSheet, ref IDomElement<TDependencyObject, TDependencyProperty> domElement, SelectorMatcher[] fragments, ref int currentIndex)
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                // TODO: maybe can use GeneralParentFailed
                return MatchResult.ItemFailed;
            }

            var thisPosition = domElement.LogicalParent?.LogicalChildNodes.IndexOf(domElement) ?? -1;

            thisPosition = (domElement.LogicalParent?.LogicalChildNodes.Count ?? 0) - thisPosition;

            return CalcIsNth(factor, distance, ref thisPosition) ? MatchResult.Success : MatchResult.ItemFailed;
        }
    }
}
