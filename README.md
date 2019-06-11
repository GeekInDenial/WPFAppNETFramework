# WPFAppNETFramework
Create File list of Selected Folder you select using a Dialog box.

Creates a 'Temp.tsv' file (tab delimited file) in user's temporary folder.
Use the Windows Key + R simultaneously. Then, type '%temp%' [without the quotes] and then hit the Enter button.
Locate the 'Temp.tsv' file and open in Microsoft Excel or import into a database depending on the size.

The first two lines of the 'Temp.tsv' file is header information.

The following columns will be in the Temp.tsv file:
FileName, Extension, Directory, FileSize, CreateDateTime

Note: The Directory will be the fully qualified folder name.
i.e. H:\Backups\ComputerA\UserFiles\
