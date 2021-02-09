@ECHO ON

GOTO IS_RUNNING

:IS_RUNNING
ECHO Checking if Unity is running...
TASKLIST /fi "imagename eq Unity.exe" 2>NUL | FIND /i /n "unity.*">NUL
IF ERRORLEVEL 1 (
    ECHO Trying to run Unity batchmode
    GOTO RUN_UNITY_BATCHMODE 
) ELSE (
    ECHO Found Unity process, removing it now...
    TASKKILL /fi /f "imagename eq Unity.exe"
)

:RUN_UNITY_BATCHMODE
ECHO Attempting to open Unity in Batchmode...
SET UnityPath="D:\Programs\Unity\2019.4.19f1\Editor\Unity.exe"
SET StartupPath="D:\GIT\ARFolio\ARFolioBundler"
%UnityPath% -batchmode -quit -projectPath %StartupPath% -executeMethod CreateFolio.OnPreprocessAsset
