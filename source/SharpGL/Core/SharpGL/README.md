Mapping OpenGL Functions
========================

Check 'History of OpenGL' [1] for extensions for each version.

OpenGL Type         C# Type
---------------------------
enum                uint
intptr              int
sizeiptr            int
bitfield            uint
const uint *        uint[]      NORMALLY, e.g in glDelete[Objects]
uint *              uint[]      NORMALLY, e.g in glGen[Objects]
clampf              float
clampd              double

OpenGL typedefs
---------------

typedef int64_t GLint64;
typedef uint64_t GLuint64;
typedef struct __GLsync *GLsync;









----


[1] "History of OpenGL": https://www.opengl.org/wiki/History_of_OpenGL