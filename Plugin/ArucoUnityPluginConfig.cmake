# ===================================================================================
#  ArucoUnityPlugin CMake configuration file
#
#             ** File generated automatically, do not modify **
#
#  Usage from an external project:
#    In your CMakeLists.txt, add these lines:
#
#    FIND_PACKAGE(ArucoUnityPlugin REQUIRED)
#    TARGET_LINK_LIBRARIES(MY_TARGET_NAME ${ArucoUnityPlugin_LIBS})
#
#    If the module is found then ArucoUnityPlugin_FOUND is set to TRUE.
#
#    This file will define the following variables:
#      - ArucoUnityPlugin_LIBS          : The list of libraries to links against.
#      - ArucoUnityPlugin_LIB_DIR       : The directory where lib files are. Calling LINK_DIRECTORIES
#                                       with this path is NOT needed.
#      - ArucoUnityPlugin_INCLUDE_DIRS  : The ArucoUnityPlugin include directories.
#      - ArucoUnityPlugin_VERSION       : The version of this ArucoUnityPlugin build. Example: "2.0.1"
#      - ArucoUnityPlugin_VERSION_MAJOR : Major version part of ArucoUnityPlugin_VERSION. Example: "2"
#      - ArucoUnityPlugin_VERSION_MINOR : Minor version part of ArucoUnityPlugin_VERSION. Example: "0"
#      - ArucoUnityPlugin_VERSION_PATCH : Patch version part of ArucoUnityPlugin_VERSION. Example: "1"
#
# ===================================================================================
SET(ArucoUnityPlugin_INCLUDE_DIRS "C:/projects/arucounityplugin/build/install/include")
INCLUDE_DIRECTORIES("C:/projects/arucounityplugin/build/install/include")

SET(ArucoUnityPlugin_LIB_DIR "C:/projects/arucounityplugin/build/install/lib")
LINK_DIRECTORIES("C:/projects/arucounityplugin/build/install/lib")

SET(ArucoUnityPlugin_LIBS ArucoUnityPlugin)

SET(ArucoUnityPlugin_FOUND TRUE)

SET(ArucoUnityPlugin_VERSION        2.0.1)
SET(ArucoUnityPlugin_VERSION_MAJOR  2)
SET(ArucoUnityPlugin_VERSION_MINOR  0)
SET(ArucoUnityPlugin_VERSION_PATCH  1)
