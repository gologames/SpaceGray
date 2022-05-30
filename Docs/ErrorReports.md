# Error reports

When something went wrong the **Error** button becomes visible. Click on it to display the error tab

![Error button](Images/error_button.gif)

## Errot tab

The error tab displays error reports of three severity types: **error**, **warning** and **info**

![Error tab](Images/error_tab.gif)

Click the **Color** button to colorize reports according to its severity 

## Warning severity

**Warnings** is most common type of reports. Its mean something happened that brings the internal file system structure in partically incorrect state. But mostly displayed file system structure is correct

![Warning report](Images/warning_report.gif)

## Info severity

**Info** report means that application unable to perform some operation, but this has no effect on the internal file system structure. For example operating system reported that deleted file's name is changed

![Info report](Images/info_report.gif)

## Error severity

When the application is unable to continue watching file system changes to keep the internal file system structure in correct state, the **error** report occurs

![Error report](Images/error_report.gif)

For example, you are exploring a remote file system and the Internet connection is interrupted

## Copy report text

**Double click** on a report item to copy its text to clipboard. Typically this is a file system path that could not be processed

![Copy report text](Images/error_copy.gif)

## Remove reports

**Right click** on a report item to remove it. Right click on a group of reports to remove the group. Right click on the root node to clear the error tab

![Remove reports](Images/error_remove.gif)

## See also
- [Navigation](Navigation.md)
- [Mark mode](MarkMode.md)
- [Overview](../README.md)