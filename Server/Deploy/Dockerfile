FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /App

COPY . .

WORKDIR Server/GameServer

RUN sed -i '/<Target Name="PreBuild"/,/<\/Target>/d' GameServer.csproj \
 && sed -i '/<Target Name="PostBuild"/,/<\/Target>/d' GameServer.csproj

RUN dotnet build

RUN chmod +x EntryPoint.sh

ENTRYPOINT ["./EntryPoint.sh"]
CMD ["dotnet", "run"]