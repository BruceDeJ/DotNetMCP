# MCP Server

This README was created using the C# MCP server project template.
It demonstrates how you can easily create an MCP server using C# and publish it as a NuGet package.

The MCP server is built as a self-contained application and does not require the .NET runtime to be installed on the target machine.
However, since it is self-contained, it must be built for each target platform separately.
By default, the template is configured to build for:
* `win-x64`
* `win-arm64`
* `osx-arm64`
* `linux-x64`
* `linux-arm64`
* `linux-musl-x64`

## Developing locally

To test this MCP server from source code (locally) without using a built MCP server package, you can configure your IDE to run the project directly using `dotnet run`.

```json
{
  "servers": {
    "OrdersMCPServer": {
      "type": "stdio",
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "<PATH TO PROJECT DIRECTORY>"
      ]
    }
  }
}
```

Refer to the VS Code or Visual Studio documentation for more information on configuring and using MCP servers:

- [Use MCP servers in VS Code (Preview)](https://code.visualstudio.com/docs/copilot/chat/mcp-servers)
- [Use MCP servers in Visual Studio (Preview)](https://learn.microsoft.com/visualstudio/ide/mcp-servers)

## Configuring the MCP server in Claude Desktop

To configure the MCP server in Claude Desktop you can follow the below steps:
* Click File
* Click Settings
* Under Desktop App -> Select Developer
* You should see "Local MCP Servers", click on Edit Config
* This should open up a directory within Claude, in this directory you should see a file called claude_desktop_config.json, open it
* Add the below section

```json
  "mcpServers": {
        "OrderService": {
          "type": "stdio",
          "command": "dotnet",
          "args": [
            "run",
            "--project",
            "C:\\Repositories\\Personal\\DotNetMCP\\OrdersMCPServer" -- Note this could be different for you
          ],
          "env": {}
        }
      }
```

* Restart Claude
* Once Claude is back online, in the chat window click on the "+" icon
* Click on Connectors
* You should now see the order service connector
* If you dont then click on "Manage Connectors", You may see the order service there and it should display any errors
* Follow this [link](https://code.claude.com/docs/en/mcp-quickstart#edit-mcp-json-directly) for more info on how to setup MCP servers in Claude

## Testing the MCP Server

Firstly run the OrderSystem API server, then make sure you dont have any other AI agents/tools trying to use the MCP server
such as Claude desktop, if there are it will fail with a file locked error.

You can use MCP inspector to test the app before wiring it up to a AI agent/tool. 

Simply open up cmd/powershell and run the below command:
npx @modelcontextprotocol/inspector dotnet run --project path_to_mcp_csproj_file

Example:
npx @modelcontextprotocol/inspector dotnet run --project C:\Repositories\Personal\DotNetMCP\OrdersMCPServer


## More information

.NET MCP servers use the [ModelContextProtocol](https://www.nuget.org/packages/ModelContextProtocol) C# SDK. For more information about MCP:

- [Official Documentation](https://modelcontextprotocol.io/)
- [Protocol Specification](https://spec.modelcontextprotocol.io/)
- [GitHub Organization](https://github.com/modelcontextprotocol)
- [MCP C# SDK](https://modelcontextprotocol.github.io/csharp-sdk)
