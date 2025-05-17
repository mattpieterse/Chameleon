#define AppName "Chameleon"
#define AppExeName "chameleon.exe"
#define AppVersion "2025w20b"


[Setup]

AppName={#AppName}
AppVersion={#AppVersion}
DefaultDirName={commonpf}\{#AppName}
DefaultGroupName={#AppName}
OutputBaseFilename=chameleon-installer-x64
OutputDir=.\bin\Installer
Compression=lzma
SolidCompression=yes
AlwaysRestart=yes


[Languages]

Name: "english"; MessagesFile: "compiler:Default.isl"


[Files]

Source: ".\bin\Release\net8.0\win-x64\chameleon.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\bin\Release\net8.0\win-x64\*.*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs


[Registry]

Root: HKLM; Subkey: "SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\{#AppExeName}"; \
    ValueType: string; ValueName: ""; ValueData: "{app}\{#AppExeName}"; Flags: uninsdeletekey
    
Root: HKLM; Subkey: "SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\{#AppExeName}"; \
    ValueType: string; ValueName: "Path"; ValueData: "{app}"; Flags: uninsdeletekey

Root: HKLM; Subkey: "SYSTEM\CurrentControlSet\Control\Session Manager\Environment"; \
    ValueType: expandsz; ValueName: "Path"; ValueData: "{olddata};{app}"; Flags: preservestringtype

    
[Run]

Filename: "{app}\{#AppExeName}"; Description: "Launch {#AppName}"; Flags: nowait postinstall skipifsilent