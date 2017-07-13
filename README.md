Basic repo with templates for .NET Core

#### usefull links on how to start with creating custom template

https://github.com/dotnet/templating
https://rehansaeed.com/custom-project-templates-using-dotnet-new/

#### Install Templates

Pull the repo and 

```
dotnet new -i micro-basic
dotnet new -i micro-job
dotnet new -i micro-job-rabbit
```

#### Using Templates

```
dotnet new micro-basic --name <name> -o <folderOutpt>
```

#### To Get Help
```
dotnet new micro-basic -h
```

#### To reset list of installed templates

```
dotnet new --debug:reinit
```
