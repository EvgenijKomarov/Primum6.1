export const api = {
  public: {
    login: "/public/login",
    register: "/public/register",
  },
  user: {
    getUserInfo: "/user/",
    sendEmailVerification: "/user/send-email-verification",
    confirmEmail: "/user/confirm-email-verification",
    confirmChat: "/user/confirm-chat",
    createTeacherProfile: "/user/create-teacher-profile",
    createStudentProfile: "/user/create-student-profile",
  },
  student: {
    getProfile: "/student",
  },
  teacher: {
    getProfile: "/teacher",
  },
  teacherCourse: {
    getCourses: "/teacher/courses",
  },
  publicTheme: {
    getThemes: "/public/themes",
  },
}