# FileWarden
Application for file system bulk operations.
<br/>
Supported operations
- Rename

# FileWarden.Cli

## Rename

  --source           Required. Source directory

  --suffix           Required. Suffix to append to file name

  -r, --recursive    (Default: false) When 'true' it will rename files only in top level directory, otherwise it will
                     rename all inner files

  -b, --backup       (Default: false) When 'true' it will create backup directory with the original directory

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
