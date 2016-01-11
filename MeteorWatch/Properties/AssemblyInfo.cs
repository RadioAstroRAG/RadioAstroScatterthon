using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Scatterthon")]
[assembly: AssemblyDescription("Developed by Victoria Penrice and Chris Jackson." + "\r\n" + 
    "\r\n" + "Simplifies the processing of meteor data inline with the objectives of the British Astronomical Association, Radio Astronomy Group (BAA RAG) Director Paul Hyde and his Scatterthon initiative, as published in BBC 'Sky at Night' magazine in 2014." + 
    "\r\n" + "\r\n" + "Sincere thanks to Paul Hyde, Tony Abbey and Fred Hopper for their dedicated testing and invaluable feedback." +
    "\r\n" + "\r\n" + "Please email info@radioastro.org.uk with your comments.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Northern Lights")]
[assembly: AssemblyProduct("Scatterthon")]
[assembly: AssemblyCopyright("Copyright ©  2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("411702de-18c6-4113-8963-cf46db9df994")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.3.4.25")]
[assembly: AssemblyFileVersion("1.3.4.25")]

/// Change history
/// 
// 1.2.4.25 - 11/01/2016
//
// Now handling incorrectly formatted log files with some grace...
//
// 1.2.4.24 - 24/12/2015
//
// Refactored RMOB and Preview code.
// Added extra tooltips...
// Restored Rmob import and export buttons.
// Added ability to preview colorgramme change on temporary top 
// (events per hour) count change (Show button in RMOB tab)...
//
// 1.2.4.23 - 23/12/2015
//
// Added scrolling to popout images...
// Fixed config changes saving and reloading...
//
// 1.2.4.22 - 21/12/2015
//
// Fixed bug where the first screenshot was not displayed on "next" log...
//
// Multiple selection now only has an image not showing if at least one of the selected rows
// does not contain a reference to the images selected in the first selected row.
//
// Combined Satellite and ISS into "Satellite" category; removed "Aircraft" category.
//
// Maximised window with thumbnails now closes on Escape key press.
//
// 1.2.4.21 - 20/12/2015
//
// Added a "Save" button and a popup reminding to save on application exit...
//
// 1.2.4.20 - 11/11/2015
//
// Fixed missing/incorrect images due to time sort in saved log files...
//
// 1.2.4.16 - 27/09/2015
//
// Introduced "show column" checkboxes in config page...
//
// 1.2.4.15 - 27/09/2015
//
// Now copies image data from split duplicate...
//
// 1.2.4.14 - 27/09/2015
//
// Split rows now don't carry original data duplicates...
//
// 1.2.4.13 - 27/09/2015
//
// Preview tab now syncs with currently cleansed date...
//
// 1.2.4.12 - 26/09/2015
//
// Made picture columns visible, so they can be sorted by in the grid...
// Fixed exception on start up, where logs and screenshots folders are not where configured...
//
// 1.2.4.5 - 16/08/2015
//
// Now under source control, with station config and a new log file viewer.
//
// 1.2.4.4 - 19/10/2014
//
// Added functionality for annual top meteor count setting.
//
// 1.2.4.2 - 25/08/2014
//
// 1. Fixed orphaned images (now moved to Removed screenshots directory).
// 2. Fixed the problem with the colourgram index throwing out of bounds exception (row -1).
// 3. Removed txtCaptureDelayOverflow and the associated message to the user.
//
// 1.2.3.2 - 22/08/2014
//
// 1. Added a file filter in Helper::ValidateCounter() to fix the file indexes issue.
//
// 1.2.3.1 - 20/08/2014
//
// 1. Added console window for debugging.
//
// 1.2.2.1 - 18/08/2014
//
// 1. Fixed incorrect event count due to flowover values between hours.
//
// 1.2.1.1 - 17/08/2014
//
// 1. Colours made more consistent with RMOB palette.
// 2. Dividers removed and split containers moved into tabs.
// 3. Blank lines are no longer counted for meteor events.
// 4. More info for the user added in pop-up message boxes.
// 5. About 'Scatterthon' box added with author's names and thanks to 
//    our testers Paul Hyde, Tony Abbey and Fred Hopper.
// 6. Account for the cornercase where screenshot belongs to previous log file.
//   
// 1.1.1.1 - 16/08/2014
//
// 1. Fixed DateTime ParseExact exception in richTextBox1_MouseDoubleClick.
//
// 1.1.1.0 - 15/08/2014
//
// 1. Regarding the whole application, the user should now only be able to run one instance of the application at a time. This helps avoid conflict in accessing same file from two different processes.
// 2. In Log Files screen, updated log file versions are loaded for viewing if they exist. This helps avoid losing cleansed data on reloading the application or applying new settings.
// 3. In Log Files screen, the highlighted entry is now second from the top, as opposed to being the very top one. This enables the user to more easily select the log entry preceding the current event (i.e. event captured in the currently viewed screenshot).
// 4. In Config screen, individual "Apply" buttons added for updating numeric settings (such as screenshot capture delay). This offers a "lightweight" configuration change, and helps avoid having to reload and re-index all the log files and their corresponding screenshots (which happens when we click "Reload" in Config screen).
// 5. In Log Files screen, more rigour applied in cases of multiple "cleans" on the same log file (multiple "Clean" button cicks) - thi was specific to the first hour in the log file. Further testing required. 
//
// 1.1.0.0 - 14/08/2014
//
// 1. User can click on a log entry and see the corresponding image displayed. Catch: the user will still need to click "Sync" after they've moved onto a different file, to index the images.
// 2. The "Reload" button is now anchored to the bottom of the window, so it won't get covered on resize of the main application window.
// 3. The file structure is now created directly on Desktop, so people can work with familiar directories (as opposed to having to search for hidden application folders). 
//