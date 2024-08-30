# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY *.sln ./
COPY Presentation/Presentation.csproj Presentation/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
RUN dotnet restore Presentation/Presentation.csproj

# Copy the rest of the application code
COPY . .

# Build the application
WORKDIR /src/Presentation
RUN dotnet build Presentation.csproj -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish Presentation.csproj -c Release -o /app/publish /p:UseAppHost=false

# Use the official .NET ASP.NET runtime image for the final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Set the working directory inside the container
WORKDIR /app

# Copy the published application from the build container
COPY --from=publish /app/publish .

# Expose the ports the application will run on
EXPOSE 8080
EXPOSE 8081

# Define the entry point for the container
ENTRYPOINT ["dotnet", "Presentation.dll"]
