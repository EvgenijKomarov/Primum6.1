export const api = {
  public: {
    login: "/public/login",
    register: "/public/register",
  },
  user: {
    getUserInfo: "/user/",
    sendEmailVerification: "/user/send-email-verification",
    confirmEmail: "/user/confirm-email",
    confirmChat: "/user/confirm-chat",
    createTeacherProfile: "/user/create-teacher-profile",
    createStudentProfile: "/user/create-student-profile",
  },
  student: {
    getProfile: "/student",
    subscribe: "/student/subscribe",
  },
  studentLesson: {
    getAll: "/student/lessons",
    getFuture: "/student/lessons/future",
  },
  teacher: {
    getProfile: "/teacher",
  },
  publicTeacher: {
    getAll: "/public/teachers",
    getSchedules: "/public/teachers",
  },
  teacherCourse: {
    getCourses: "/teacher/courses",
  },
  publicTheme: {
    getThemes: "/public/themes",
  },
  publicCourse: {
    getAll:     "/public/courses",
    getByTheme: "/public/courses/by-theme",
  },
  teacherSchedule: {
    base: "/teacher/shedules",
  },
}