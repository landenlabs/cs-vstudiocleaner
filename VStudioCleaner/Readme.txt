
VStudio Cleaner v1.8  2011
By Dennis Lang 


VStudioCleaner was created to remove all the droppings from Visual Studio 2008.
The default filters will optionally remove the following files:

	1. "bin" directory
	2. "obj" directory
	3. "Debug" directory
	4. "Release" directory
	5. "Backup" directory
	6. "_UpgradeReport_Files" directory
	7. *.pdb, *.ncb, *.ilk, *.suo  files
	8. setting*.xml, connection*.xml
	
I did a google search and found Leonardo Paneque's program "Solution Cleaner" which covers most of my requirements.

	Leonardo Paneque "Solution Cleaner"
	http://www.teknowmagic.net/

The problems with "Solution Cleaner"

	1. The filter rules are fixed
	2. The rules don't clean all of the Visual Studio 2008 auxilary files
	3. The file list cannot be sorted
	4. The file list cannot be exported
	5. The User Interface cannot be resized  (I hate programs which don't resize)
	6. Does not remove directories

The good news with "Solution Cleaner"

	1. The program does solve most of the problem of finding files and deleting them.
	2. The author provides the source code (I love source code).

While I was googling, I also found Wise Cleaner.
 
	http://www.wisecleaner.com/

Wise cleaner supported custom rules and has some nice features, but I had problems trying to select and deselect files 
from the final list and quickly gave up and decide to write my own using Solution Cleaner as a starting point.

VStudio has the following features:

	1. Built-in filters to remove files and directories (including subdirectories)
	2. Ability to add custom filters
	3. Ability to bookmark filter settings and search paths
	4. Save/Restore settings to/from registry  (I am not a big fan of Windows registry but it is a common way to saving settings)
	5. All dialogs can be resized and the main dialog has a sash to resize the panels.
	6. All list dialogs support column sorting
	7. All list dialogs can export to CSV file
	8. The 'located' list can contain both files and directories
	9. Right click on 'located' files and you can launch DOS cmd.exe, or windows Explorer

Update Dec 2011 (v1.8)
	
	1. Added color (foreground and background) per filter rule.
	2. Fixed registry setting/viewing in Windows 7
	3. Remember last bookmark and apply on startup.
	4. Place active bookmark name in title bar.
	5. Add rename pull-down to bookmarks.




