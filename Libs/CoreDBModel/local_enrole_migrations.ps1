dotnet tool install --global dotnet-ef
dotnet ef migrations bundle --self-contained -r win-x64 -o efbundle.exe --project CoreDBModel.csproj

# Получаем строку подключения
$CONN = Invoke-RestMethod -Uri "http://localhost:5000/variable/CoreDatabaseConnection"

# Накатываем миграции
.\efbundle.exe --connection $CONN