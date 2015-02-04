Mapping OpenGL Functions
========================

OpenGL Type         C# Type
---------------------------
enum                uint
intptr              int
sizeiptr            int
bitfield            uint
const uint *        uint[]      NORMALLY, e.g in glDelete[Objects]
uint *              uint[]      NORMALLY, e.g in glGen[Objects]

OpenGL typedefs
---------------

typedef int64_t GLint64;
typedef uint64_t GLuint64;
typedef struct __GLsync *GLsync;