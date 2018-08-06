using System;
using System.Collections.Generic;
using System.Linq;

namespace Domini
{
    public class UnitTestDetector
    {
        public static readonly HashSet<string> UnitTestAttributes = new HashSet<string>
        {
            "microsoft.visualstudio.testtools.unittesting",
            "nunit.framework",
        };

        public static bool IsInUnitTest { get; private set; }

        static UnitTestDetector()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Select(a => a.FullName.ToString());
            if ((from assembly in assemblies
                from unitTestAttribute in UnitTestAttributes
                where assembly.StartsWith(unitTestAttribute)
                select assembly).Any())
            {
                IsInUnitTest = true;
            }
        }
    }
}