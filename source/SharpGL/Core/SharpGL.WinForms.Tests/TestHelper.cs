using System;
using System.Globalization;
using System.IO;
using System.Threading;

namespace SharpGL.WinForms.Tests
{
    public static class TestHelper
    {
        private sealed class TemporaryCulture : IDisposable
        {
            private readonly CultureInfo savedCulture;
            private readonly CultureInfo savedUICulture;

            public TemporaryCulture(string culture) : this(new CultureInfo(culture)) { }

            private TemporaryCulture(CultureInfo culture)
            {
                savedCulture = Thread.CurrentThread.CurrentCulture;
                savedUICulture = Thread.CurrentThread.CurrentUICulture;

                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }

            public void Dispose()
            {
                Thread.CurrentThread.CurrentCulture = savedCulture;
                Thread.CurrentThread.CurrentUICulture = savedUICulture;
            }
        }

        public static IDisposable SetCurrentCulture(string culture)
        {
            return new TemporaryCulture(culture);
        }

        public static string ResolvePath(string path)
        {
            return Path.IsPathRooted(path) ? path : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
        }
    }
}