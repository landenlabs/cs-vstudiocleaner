@echo off

@rem TODO - use drive env in script below
 
set prog=vstudiocleaner
set bindir=d:\opt\bin
set msbuild=F:\opt\VisualStudio\2022\Preview\MSBuild\Current\Bin\MSBuild.exe


@echo ---- Clean Release %prog% 
lldu -sum %prog%\obj %prog%\bin 
rmdir /s %prog%\obj  2> nul
rmdir /s %prog%\bin  2> nul
@rem %msbuild% %prog%.sln  -t:Clean

@echo.
@echo ---- Build Release %prog% 
%msbuild% %prog%.sln -p:Configuration="Release";Platform=x64 -verbosity:minimal  -detailedSummary:True

@echo.
@echo ---- Build done 
if not exist "%prog%\bin\x64\Release\%prog%.exe" (
   echo Failed to build %prog%\bin\x64\Release\%prog%.exe
   goto _end
)
 
@echo ---- Copy Release to %bindir%
copy  %prog%\bin\x64\Release\%prog%.exe %bindir%\%prog%.exe
ld -hp   %prog%\bin\x64\Release\%prog%.exe %bindir%\%prog%.exe

@rem play happy tone
rundll32.exe cmdext.dll,MessageBeepStub
rundll32 user32.dll,MessageBeep
 
:_end
