#define AppName "Chameleon"
#define AppExeName "chameleon.exe"
#define AppVersion "2025w20b"
#define AppPublisherUrl "https://www.github.com/mattpieterse"
#define AppPublisher "Matthew Pieterse"
#define AppCopyright "Copyright © 2025 Pieterse M."


[Setup]

AppName={#AppName}
AppVersion={#AppVersion}
AppPublisherURL={#AppPublisherUrl}
AppPublisher={#AppPublisher}
AppCopyright={#AppCopyright}
AppId={{BEF1C02B-9739-4DBB-A45F-4E5A6E24B5AD}}
DefaultDirName={commonpf}\{#AppName}
DefaultGroupName={#AppName}
OutputBaseFilename=chameleon-installer-x64
OutputDir=.\bin\Installer
Compression=lzma
SolidCompression=yes
AlwaysRestart=yes
PrivilegesRequired=admin


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