# Runs every time a package is installed in a project

param($installPath, $toolsPath, $package, $project)
#$project.Object.References.Add("Microsoft.SPOT.TinyCore")

# $installPath is the path to the folder where the package is installed.
# $toolsPath is the path to the tools directory in the folder where the package is installed.
# $package is a reference to the package object.
# $project is a reference to the project the package was installed to.
