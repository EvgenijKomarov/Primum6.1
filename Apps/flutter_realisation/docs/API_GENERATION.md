# API Client Generation

This document explains how to auto-generate a Dart (Dio) client into `lib/api` from a running Swagger/OpenAPI endpoint.

Requirements
- Either `openapi-generator-cli` available locally (npm package `@openapitools/openapi-generator-cli`) or Docker installed.
- Internet access to the Swagger/OpenAPI JSON (for example `http://localhost:5002/swagger/v1/swagger.json`).

Quick steps (PowerShell)

1. Install generator (one of):

```powershell
# npm global install
npm i -g @openapitools/openapi-generator-cli
```

or use Docker (no install needed besides Docker).

2. Run the provided script (from repository root):

```powershell
.\tools\generate_api.ps1 -SpecUrl "http://localhost:5002/swagger/v1/swagger.json" -OutputDir "lib/api" -Generator "dart-dio" -PackageName "my_api"
```

3. After generation, add required dependencies to `pubspec.yaml` if not already present, for example:

```yaml
dependencies:
  dio: ^5.0.0
```

Then run:

```powershell
flutter pub get
```

Notes
- The script saves the downloaded spec to `.openapi/swagger.json`.
- The default generator is `dart-dio`. You can change the generator and additional properties via the script parameters.
- Generated code will be placed in `lib/api` — review and adapt names as needed.
