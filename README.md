## How to Run the App

### Prerequisites

* **.NET 8 SDK**: Ensure you have the appropriate .NET SDK installed on your system. 

### Setup and Execution

1.  **Clone the Repository:**
     ```bash
    git clone https://github.com/bohdantiranu/ApiDataProcessorTestTask.git
    cd ApiDataProcessorTestTask
    ```

2.  **`appsettings.json` Exists**:
    Make sure `appsettings.json` file exists in the root directory of your project and has the following content:

    ```json
    {
      "ApiUrls": {
        "Users": "https://jsonplaceholder.typicode.com/users",
        "Posts": "https://jsonplaceholder.typicode.com/posts"
      }
    }
    ```

3.  **Install NuGet Packages**:
    Open your terminal or command prompt, navigate to the project directory and run the following commands:

    ```bash
    dotnet add package Microsoft.Extensions.Configuration
    dotnet add package Microsoft.Extensions.Configuration.Json
    dotnet add package Microsoft.Extensions.Configuration.Binder
    dotnet add package Microsoft.Extensions.Configuration.FileExtensions
    ```

4.  **Run the Application**:
    From the same terminal in the project directory, execute:

    ```bash
    dotnet run
    ```