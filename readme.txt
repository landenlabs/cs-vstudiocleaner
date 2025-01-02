
VStudio	Cleaner	v1.9 2011
==========================
Please read doc-web\vstudiocleaner.html	for latest notes.


VStudio	Cleaner	v1.6  2009
==========================

Author:
  Dennis Lang 2009
  https://landenlabs.com

Credits:
  This application is based off	of Leonardo Paneque "Solution Cleaner"
  http://www.teknowmagic.net/

Description:
============

VStudioCleaner was created to remove all the droppings from Visual Studio 2008.
The default filters will optionally remove the following files and directories:

	1. "bin" directory
	2. "obj" directory
	3. "Debug" directory
	4. "Release" directory
	5. "Backup" directory
	6. "_UpgradeReport_Files" directory
	7. *.pdb, *.ncb, *.ilk,	*.suo  files
	8. setting*.xml, connection*.xml

I did a	google search and found	Leonardo Paneque's program "Solution Cleaner" which covers most	of my requirements.

	Leonardo Paneque "Solution Cleaner"
	http://www.teknowmagic.net/

The problems with "Solution Cleaner"

	1. The filter rules are	fixed
	2. The rules don't clean all of	the Visual Studio 2008 auxiliary files
	3. The file list cannot	be sorted
	4. The file list cannot	be exported
	5. The User Interface cannot be	resized	(I hate	programs which don't resize)
	6. Does	not remove directories

The good news with "Solution Cleaner"

	1. The program does solve most of the problem of finding files and deleting them.
	2. The author provides the source code (Thank you Leonardo).

While I	was googling, I	also found Wise	Disk Cleaner.

	http://www.wisecleaner.com/

Wise cleaner supported custom rules and	has some nice features,	but I had problems trying to select and	deselect  filters
and files because it does not support extended or multi-select and all the sorting options I needed.  
So I just had to write my own (because that is what I do) and I	used Solution Cleaner as a starting point.

VStudio	Cleaner	has the	following features:
===========================================

	1. Built-in filters to remove files and	directories (including subdirectories).
	2. Ability to add custom filters.
	3. Ability to bookmark filter settings and search paths.
	4. Save/Restore	settings to/from registry  (I am not a big fan of Windows registry but it is a common way to saving settings).
	5. All dialogs can be resized and the main dialog has a	sash to	resize the panels.
	6. All list dialogs support column sorting.
	7. All list dialogs can	export to CSV file.
	8. The 'located' list can contain both files and directories (and the dialog is	not modal, I also hate modal dialogs).
	9. Right click on 'located' files and you can launch DOS cmd.exe, or windows Explorer.
     10. Filter	rules support file selection via extension, directory by name or wildcards for both files and directories
     11. Built-in support to register VStudioCleaner as	add-on for folders. When registered, and you right click on a folder you 
	 will see VStudioCleaner as a choice to	clean that directory.
     12. Options menu supports launching RegEdit to show current saved settings	or shell registration.


How to Use:
===========

To use VStudioCleaner you need to set the search Path to one or	more directories and select one	or more	filters.
The search "path" can be set by	either typing in the path, cut&paste a path or using the Path... button	to browse and select a path.
If you need to specify multiple	paths, you cannot use the "Path..." button because it does not append, it only knows how to set	the
search path.  Multiple directory (folder) paths	are specified by separating them by semicolon, as in:

		c:\dir1;c:\dir2\subdir1;d;\dir3\subdir2;e:\

You set	your filters by	either 'checking' or 'unchecking' the built-in filters or by adding your own.
Once the search	"Path" and "Filters" are set, press the	"Locate" button	to build up a list of files and	directories.

The resulting list will	appear in its own dialog.  You can sort	the various columns (why, directory, file, extension, size) and
right-click to remove files from the list.  The	right-click remove action only removes the names from the list,	it does	not 
remove anything	from your computer.  Once you have your	list of	files pruned down to those you want to delete from your	computer, 
you can	press the "delete" button.  Please see Warning notice below.


Filters:
========

Filters	are patterns used to select directories	and files.

Filters	fall into three	categories:
	1.	Directory names, match directory and select all	files and subdirectories
	2.	File extensions, matches files
	3.	Wildcard, matches directories and files

Directory filters must end in a	back slash. Example:   debug\ 
Directory filters only match on	directory names	and cause the directory	and all	of its subdirectories and files	to be selected.

File extension must start with a decimal point.	Example:  .obj
File extension filter only matches files.

Wildcard is a collection of characters with one	or more	wildcard character '*'.	
Each wildcard can appear multiple times.

	*   matches zero or more characters.
	Example:  Connect*.xml
	Example:  *foo*bar.*	

The wildcard filter is tested against the fulll	file or	directory path so it is	common for 
all wildcard patterns to start with * to match any prefix of disk drive	letters	and 
directories.

	Example
	Pattern		      Test Against			   Results
	--------------------  ----------------------------------   ----------
	foo*		      foobarcar				   match
	foo*		      carbarfoo				   no match
	*foo		      carbarfoo				   match
	*foo		      c:\alpha\beta\foo			   match
	*foo\bin\*	      foo\junk				   no match
	*foo\bin\*	      foo\bin\				   match
	*foo\bin\*	      c:\alpha\foo\bin\			   match
	*\bin\*.obj	      c:\foo\bar\bin\car.obj		   match
	*\bin\*.o*	      c:\foo\bar\bin\car.old		   match
	*\bin\*.o*	      d:\bin\junk.obj			   match

A filter can contain one or more filters separated by semicolons.
Example:    Connect*.xml;.obj;obj\

Exclude	filters	start with a minus sign, as in:	 -*.exe
Exclude	filters	only work with wildcard	filters.
If the filter name starts with "never" it will be executed after other matches to trim any excluded matches.



Use "Add" and "Delete" buttons to manage your collection of filter rules.
Double click on	a row to change	its value.
Right click if you want	to Export the filter rules as CSV
Filter names must be unique.  On startup any duplicate filters will be ignoored.

Bookmarks:
==========

Use the	bookmarks to memorize a	set of filter rule checkmark states and	Path setting.
Use the	bookmark Add button to add a new bookmark to remember the current settings. 
Double Click on	a bookmark to activate its settings.
Press the 'Set'	button to refresh the current state of a selected bookmark to the current list of 
active filter rules and	Path.

Keyboard:
=========
      Control-A	to select entire List
      Space to toggle check boxes
      Delete key to delete selected items or item under	mouse
      Up/Down to navigate up/down list and select row.

Warning:
========
No undo	to restore items removed from a	list or	any undo to restored DELETED files or directories.
Deleted	files and directories will not appear in your recycle bin so you have no way to	undo your deletions. 



Copyright:
==========

Feel free to use source	code as-is or modified.
Please give me credit if you use the source code.
I offer	no support or protection from any problems created by this code.



