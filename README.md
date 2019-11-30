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

  --help             Display this help screen.

  --version          Display version information.

## Examples
<br/>
`warden rename --source "C:\test" --suffix "_1" --recursive --backup`
<br/>
<br/>
`warden rename --source "C:\test" --suffix "_1" -rb`
<br/>
<br/>
`warden rename --source "C:\test" --suffix "_1" -rb --no-cleanup`