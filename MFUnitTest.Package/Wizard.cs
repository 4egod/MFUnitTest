/*
using Microsoft.Build.Evaluation;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.TemplateWizard;
using NuGet.VisualStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFUnitTest_2019
{
    public class Wizard : IWizard
    {
        IEnumerable<string> _packages;

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            if (customParams.Length > 0)
            {
                var vstemplate = XDocument.Load((string)customParams[0]);
                _packages = vstemplate.Root
                    .ElementsNoNamespace("WizardData")
                    .ElementsNoNamespace("packages")
                    .ElementsNoNamespace("package")
                    .Select(e => e.Attribute("id").Value)
                    .ToList();
            }
        }

        public void ProjectFinishedGenerating(Project project)
        {
            var componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));
            var _installer = componentModel.GetService<IVsPackageInstaller2>();

            foreach (var package in _packages)
            {
                _installer.InstallLatestPackage(null, project, package, false, false);
            }
        }
    }
}
*/