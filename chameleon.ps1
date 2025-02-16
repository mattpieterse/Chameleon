# Copyright (C) 2025  Matthew Pieterse
#
# This program is free software: you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.  See LICENSE for more details.

# -- Functions

function Get-GitInstallation {
    [CmdletBinding()]
    param()

    try {
        $gitVersion = git --version
        return @{
            IsInstalled = $true
            Version = $gitVersion
        }
    }
    catch {
        return @{
            IsInstalled = $false
            Version = $null
        }
    }
}

# -- Execution

$gitInstallation = Get-GitInstallation
if (-not($gitInstallation.IsInstalled)) {
    throw("Git is not installed or not in PATH")
}