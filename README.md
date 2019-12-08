# FileWarden
Application for file system bulk operations.
<br/>
Supported operations
- Rename

# FileWarden.Cli

## Rename

  --source           Required. Source directory

  --prefix           (Group: rename-append) Prefix to append to file name

  --suffix           (Group: rename-append) Suffix to append to file name

  -r, --recursive    (Default: false) When 'true' it will rename files only in top level directory, otherwise it will
                     rename all inner files

  --backup           Backup directory path. Default location is %temp%

  --no-backup        (Default: false) When 'true' it will not create backup directory

  --no-cleanup       (Default: false) When 'true' it will not delete backup directory

  -f, --force        (Default: false) When 'true' it will overwrite existing files with the same name after applying
                     suffix / prefix

  --help             Display this help screen.

  --version          Display version information.

## Examples
```
warden rename --source "C:\test" --suffix "_1" --recursive --backup
```
```
warden rename --source "C:\test" --suffix "_1" -rb
```
```
warden rename --source "C:\test" --suffix "_1" -rb --no-cleanup
```
