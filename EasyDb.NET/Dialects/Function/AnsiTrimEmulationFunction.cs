﻿using System;

namespace LX.EasyDb.Dialects.Function
{
    /// <summary>
    /// </summary>
    public class AnsiTrimEmulationFunction : AbstractAnsiTrimEmulationFunction
    {
        /// <summary/>
        public static readonly String LTRIM = "ltrim";
        /// <summary/>
        public static readonly String RTRIM = "rtrim";
        /// <summary/>
        public static readonly String REPLACE = "replace";
        /// <summary/>
        public static readonly String SPACE_PLACEHOLDER = "${space}$";

        /// <summary/>
        public static readonly String LEADING_SPACE_TRIM_TEMPLATE = LTRIM + "(?1)";
        /// <summary/>
        public static readonly String TRAILING_SPACE_TRIM_TEMPLATE = RTRIM + "(?1)";
        /// <summary/>
        public static readonly String BOTH_SPACE_TRIM_TEMPLATE = LTRIM + "(" + RTRIM + "(?1))";
        /// <summary/>
        public static readonly String BOTH_SPACE_TRIM_FROM_TEMPLATE = LTRIM + "(" + RTRIM + "(?2))"; //skip the FROM keyword in params

        /// <summary>
        /// A template for the series of calls required to trim non-space chars from the beginning of text.
        /// </summary>
        public static readonly String LEADING_TRIM_TEMPLATE =
            REPLACE + "(" +
                REPLACE + "(" +
                    LTRIM + "(" +
                        REPLACE + "(" +
                            REPLACE + "(" +
                                "?1," +
                                "' '," +
                                "'" + SPACE_PLACEHOLDER + "'" +
                            ")," +
                            "?2," +
                            "' '" +
                        ")" +
                    ")," +
                    "' '," +
                    "?2" +
                ")," +
                "'" + SPACE_PLACEHOLDER + "'," +
                "' '" +
            ")";

        /// <summary>
        /// A template for the series of calls required to trim non-space chars from the end of text.
        /// </summary>
        public static readonly String TRAILING_TRIM_TEMPLATE =
            REPLACE + "(" +
                REPLACE + "(" +
                    RTRIM + "(" +
                        REPLACE + "(" +
                            REPLACE + "(" +
                                "?1," +
                                "' '," +
                                "'" + SPACE_PLACEHOLDER + "'" +
                            ")," +
                            "?2," +
                            "' '" +
                        ")" +
                    ")," +
                    "' '," +
                    "?2" +
                ")," +
                "'" + SPACE_PLACEHOLDER + "'," +
                "' '" +
            ")";

        /// <summary>
        /// A template for the series of calls required to trim non-space chars from both the beginning and the end of text.
        /// </summary>
        public static readonly String BOTH_TRIM_TEMPLATE =
            REPLACE + "(" +
                REPLACE + "(" +
                    LTRIM + "(" +
                        RTRIM + "(" +
                            REPLACE + "(" +
                                REPLACE + "(" +
                                    "?1," +
                                    "' '," +
                                    "'" + SPACE_PLACEHOLDER + "'" +
                                ")," +
                                "?2," +
                                "' '" +
                            ")" +
                        ")" +
                    ")," +
                    "' '," +
                    "?2" +
                ")," +
                "'" + SPACE_PLACEHOLDER + "'," +
                "' '" +
            ")";

        private readonly ISQLFunction leadingSpaceTrim;
        private readonly ISQLFunction trailingSpaceTrim;
        private readonly ISQLFunction bothSpaceTrim;
        private readonly ISQLFunction bothSpaceTrimFrom;

        private readonly ISQLFunction leadingTrim;
        private readonly ISQLFunction trailingTrim;
        private readonly ISQLFunction bothTrim;

        /// <summary/>
        public AnsiTrimEmulationFunction()
            : this(LTRIM, RTRIM, REPLACE)
        { }

        /// <summary/>
        public AnsiTrimEmulationFunction(String ltrimFunctionName, String rtrimFunctionName, String replaceFunctionName)
        {
            leadingSpaceTrim = new SQLFunctionTemplate(
                    DbType.String,
                    LEADING_SPACE_TRIM_TEMPLATE.Replace(LTRIM, ltrimFunctionName)
            );

            trailingSpaceTrim = new SQLFunctionTemplate(
                    DbType.String,
                    TRAILING_SPACE_TRIM_TEMPLATE.Replace(RTRIM, rtrimFunctionName)
            );

            bothSpaceTrim = new SQLFunctionTemplate(
                    DbType.String,
                    BOTH_SPACE_TRIM_TEMPLATE.Replace(LTRIM, ltrimFunctionName)
                            .Replace(RTRIM, rtrimFunctionName)
            );

            bothSpaceTrimFrom = new SQLFunctionTemplate(
                    DbType.String,
                    BOTH_SPACE_TRIM_FROM_TEMPLATE.Replace(LTRIM, ltrimFunctionName)
                            .Replace(RTRIM, rtrimFunctionName)
            );

            leadingTrim = new SQLFunctionTemplate(
                    DbType.String,
                    LEADING_TRIM_TEMPLATE.Replace(LTRIM, ltrimFunctionName)
                            .Replace(RTRIM, rtrimFunctionName)
                            .Replace(REPLACE, replaceFunctionName)
            );

            trailingTrim = new SQLFunctionTemplate(
                    DbType.String,
                    TRAILING_TRIM_TEMPLATE.Replace(LTRIM, ltrimFunctionName)
                            .Replace(RTRIM, rtrimFunctionName)
                            .Replace(REPLACE, replaceFunctionName)
            );

            bothTrim = new SQLFunctionTemplate(
                    DbType.String,
                    BOTH_TRIM_TEMPLATE.Replace(LTRIM, ltrimFunctionName)
                            .Replace(RTRIM, rtrimFunctionName)
                            .Replace(REPLACE, replaceFunctionName)
            );
        }

        /// <inheritdoc/>
        protected override ISQLFunction ResolveBothSpaceTrimFunction()
        {
            return bothSpaceTrim;
        }

        /// <inheritdoc/>
        protected override ISQLFunction ResolveBothSpaceTrimFromFunction()
        {
            return bothSpaceTrimFrom;
        }

        /// <inheritdoc/>
        protected override ISQLFunction ResolveLeadingSpaceTrimFunction()
        {
            return leadingSpaceTrim;
        }

        /// <inheritdoc/>
        protected override ISQLFunction ResolveTrailingSpaceTrimFunction()
        {
            return trailingSpaceTrim;
        }

        /// <inheritdoc/>
        protected override ISQLFunction ResolveBothTrimFunction()
        {
            return bothTrim;
        }

        /// <inheritdoc/>
        protected override ISQLFunction ResolveLeadingTrimFunction()
        {
            return leadingTrim;
        }

        /// <inheritdoc/>
        protected override ISQLFunction ResolveTrailingTrimFunction()
        {
            return trailingTrim;
        }
    }
}
