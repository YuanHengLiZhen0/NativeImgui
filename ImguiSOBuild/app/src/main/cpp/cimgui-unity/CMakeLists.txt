cmake_minimum_required(VERSION 3.1)
project(cimgui-unity)

set (_cimgui_path ${CMAKE_CURRENT_SOURCE_DIR}/../cimgui/)

# general settings
file(GLOB IMGUI_SOURCES
    imconfig_unity.h
    plugin_custom_assert.cpp
    ${_cimgui_path}cimgui.cpp
    ${_cimgui_path}imgui/imgui.cpp
    ${_cimgui_path}imgui/imgui_draw.cpp
    ${_cimgui_path}imgui/imgui_demo.cpp
    ${_cimgui_path}imgui/imgui_widgets.cpp
)

set(IMGUI_STATIC "no" CACHE STRING "Build as a static library")

# add library and link SHARED
#if (IMGUI_STATIC)
#    add_library(${PROJECT_NAME} STATIC ${IMGUI_SOURCES})
#else (IMGUI_STATIC)
#    add_library(${PROJECT_NAME} STATIC ${IMGUI_SOURCES})
#endif (IMGUI_STATIC)


if (APPLE)
    if (IOS)
    	set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -fembed-bitcode")
        set(CMAKE_C_FLAGS "${CMAKE_C_FLAGS} -fembed-bitcode")
    	ADD_Library(${PROJECT_NAME}  ${IMGUI_SOURCES})
		set_xcode_property (${PROJECT_NAME} IPHONEOS_DEPLOYMENT_TARGET "7.0" "all")
    else ()
		ADD_Library(${PROJECT_NAME}  MODULE ${IMGUI_SOURCES})
		set_target_properties(${PROJECT_NAME}  PROPERTIES BUNDLE TRUE)
	endif ()
elseif (ANDROID)
	ADD_Library(${PROJECT_NAME}  SHARED ${IMGUI_SOURCES})
else ()
	ADD_Library(${PROJECT_NAME}  MODULE ${IMGUI_SOURCES})
	set_target_properties(${PROJECT_NAME}  PROPERTIES BUNDLE TRUE)
endif ()



# export cpp imgui functions for other libraries eg cimgui-freetype
target_compile_definitions(${PROJECT_NAME} PRIVATE IMGUI_DISABLE_OBSOLETE_FUNCTIONS=1)
if (WIN32)
    target_compile_definitions(${PROJECT_NAME} PRIVATE IMGUI_API=__declspec\(dllexport\))
    target_compile_definitions(${PROJECT_NAME} PRIVATE IMGUI_IMPL_API="extern \"C\" __declspec\(dllexport\) ")
elseif(IOS)
    target_compile_definitions(${PROJECT_NAME} PRIVATE IMGUI_API_IOS)
    #target_compile_definitions(${PROJECT_NAME} PRIVATE IMGUI_IMPL_API="extern \"C\" ")
elseif (ANDROID)
    target_compile_definitions(${PROJECT_NAME} PRIVATE IMGUI_API=)
else()
    target_compile_definitions(${PROJECT_NAME} PRIVATE IMGUI_API=)
    target_compile_definitions(${PROJECT_NAME} PRIVATE IMGUI_IMPL_API="extern \"C\" ")
endif()

target_compile_definitions(${PROJECT_NAME} PRIVATE IMGUI_USER_CONFIG="imconfig_unity.h")

target_include_directories(${PROJECT_NAME} PUBLIC ${CMAKE_CURRENT_SOURCE_DIR})
target_include_directories(${PROJECT_NAME} PUBLIC ${_cimgui_path})
target_include_directories(${PROJECT_NAME} PUBLIC ${_cimgui_path}imgui)

set_target_properties(${PROJECT_NAME} PROPERTIES PREFIX "")
set_target_properties(${PROJECT_NAME} PROPERTIES OUTPUT_NAME "cimgui")

# disable non UDT warnings
if (WIN32)
    target_compile_options(${PROJECT_NAME} PRIVATE /wd4190)
else (WIN32)
    target_compile_options(${PROJECT_NAME} PRIVATE -Wno-return-type-c-linkage)
endif (WIN32)

# install
install(TARGETS ${PROJECT_NAME}
    RUNTIME DESTINATION  .
    LIBRARY DESTINATION  .
    ARCHIVE DESTINATION  .
)

if (ANDROID)
find_library( # Sets the name of the path variable.
        log-lib

        # Specifies the name of the NDK library that
        # you want CMake to locate.
        log)

target_link_libraries( # Specifies the target library.
        ${PROJECT_NAME}

        # Links the target library to the log library
        # included in the NDK.
        ${log-lib})
endif (ANDROID)