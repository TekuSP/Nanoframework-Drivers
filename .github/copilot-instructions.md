# Copilot instructions for this repo

## Big picture
- This repo is a collection of nanoFramework device drivers plus a sample app. Drivers live under Drivers/, while Meteostanice/ is an example firmware app that exercises sensors. See [Meteostanice/Program.cs](Meteostanice/Program.cs).
- Each driver is in its own folder under Drivers/ (for example [Drivers/TCS34725](Drivers/TCS34725)), typically with device-specific enums and helper types, and uses the namespace TekuSP.Drivers.<Device>.
- Common driver abstractions live in DriverBase and the transport-specific bases (I2C/SPI/UART). See [Drivers/DriverBaseI2C/DriverBaseI2C.cs](Drivers/DriverBaseI2C/DriverBaseI2C.cs), [Drivers/DriverBaseSPI/DriverBaseSPI.cs](Drivers/DriverBaseSPI/DriverBaseSPI.cs), and [Drivers/DriverBaseUART/DriverBaseUART.cs](Drivers/DriverBaseUART/DriverBaseUART.cs).

## Driver conventions (patterns to follow)
- Drivers inherit the transport base class and implement `IDriverBase` behaviors: `Start()`, `Stop()`, `ReadData(...)`, `WriteData(...)`, `ReadDeviceId()`, `ReadManufacturerId()`, `ReadSerialNumber()`. See [Drivers/TCS34725/TCS34725.cs](Drivers/TCS34725/TCS34725.cs).
- `Start()` and `Stop()` overrides should call `base.Start()` / `base.Stop()` to initialize or dispose the underlying bus device, then perform device-specific setup/teardown (example: [Drivers/ICM20948/ICM20948.cs](Drivers/ICM20948/ICM20948.cs)).
- I2C drivers use `System.Device.I2c.I2cDevice` via `DriverBaseI2C` and typically implement `ReadRegister(...)`/`WriteRegister(...)` helpers plus unit conversions. Example: [Drivers/TCS34725/TCS34725.cs](Drivers/TCS34725/TCS34725.cs).
- Use shared enums from DriverBase for unit selection, e.g., `TemperatureUnit`, `PressureType`, `HumidityType` (see [Drivers/DriverBase/Enums](Drivers/DriverBase/Enums)).
- Communication type is tracked by `CommunicationType` (see [Drivers/DriverBase/Enums/CommunicationType.cs](Drivers/DriverBase/Enums/CommunicationType.cs)).
- If a helper, interface, or enum is not device-specific, move it into DriverBase (or the transport-specific base) instead of keeping it in a single driver.
- Prefer using shared helpers/event handlers from DriverBase (and extend them if generally applicable) instead of duplicating math/utility logic in individual drivers. See [Drivers/DriverBase/Helpers/BitHelper.cs](Drivers/DriverBase/Helpers/BitHelper.cs) and [Drivers/DriverBase/Event Handlers](Drivers/DriverBase/Event%20Handlers).
- Prefer putting enums and constants into dedicated folders (Enums/, Constants/) inside each driver; newer drivers follow this pattern (example: [Drivers/TCS34725/Enums](Drivers/TCS34725/Enums)), older ones may not yet.
- Do not use partial classes in drivers. Keep enums/constants as standalone types in the driver namespace (or a Constants/Enums namespace) and update references explicitly.
- Add XML documentation comments for public (and where meaningful internal) types, members, enums, and constants. Use existing comments, datasheets, or device knowledge to describe units, ranges, and behavior.
- Expose device enums/constants as public (consistent access), unless there is a strong reason to keep them non-public.
- You may use `/// <inheritdoc/>` where appropriate (interfaces/base classes) to satisfy XML documentation and reduce duplication.

## App usage pattern
- The sample app configures ESP32 pin functions using `nanoFramework.Hardware.Esp32.Configuration.SetPinFunction(...)`, then constructs a driver, calls `Start()`, and reads data in a loop. See [Meteostanice/Program.cs](Meteostanice/Program.cs).

## Project structure / tooling
- Projects are nanoFramework .nfproj files (not standard .csproj). See [Meteostanice/Meteostanice.nfproj](Meteostanice/Meteostanice.nfproj) and [Drivers/DriverBase/DriverBase.nfproj](Drivers/DriverBase/DriverBase.nfproj).
- Build uses MSBuild with the nanoFramework VS Code extension (msbuild is provided/configured by the extension, not necessarily on PATH). The extension runs a command like:
	- `$path = & "${env:ProgramFiles(x86)}\microsoft visual studio\installer\vswhere.exe" -products * -latest -prerelease -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\amd64\MSBuild.exe | select-object -first 1; & "C:\nf-interpreter\nuget.exe" restore "c:/Users/richa/source/repos/Meteostanice-CSharp/Meteostanice.sln"; & $path "c:/Users/richa/source/repos/Meteostanice-CSharp/Meteostanice.sln" -p:platform="Any CPU" -p:Configuration="Debug" "-p:NanoFrameworkProjectSystemPath=c:/Users/richa/.vscode/extensions/nanoframework.vscode-nanoframework-1.0.215/dist/utils/nanoFramework/v1.0/" -p:NFMDP_PE_Verbose=false -p:NFMDP_PE_VerboseMinimize=false -verbosity:minimal`
- NuGet packaging is done via PowerShell script that runs nuget.exe pack against all .nuspec files. See [GenerateNugets.ps1](GenerateNugets.ps1).

## Workflow expectations
- Plan the work using a short todo list before making changes.
- Run a build after finishing the task to verify the result.

## Integration points
- Hardware access uses nanoFramework libraries (System.Device.I2c, System.Device.Spi, System.IO.Ports) and ESP32 pin muxing via nanoFramework.Hardware.Esp32.
- Drivers often define device-specific enums/flags under each driver folder (for example [Drivers/TCS34725/Enums](Drivers/TCS34725/Enums)).
- Keep XML docs aligned with the sensor’s datasheet (timings, units, limits) and include short summaries on registers/commands where possible.
