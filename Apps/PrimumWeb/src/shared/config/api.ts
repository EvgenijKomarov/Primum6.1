export const api = {
  public: {
    login: "/public/login",
    register: "/public/register",
  },
  user: {
    getUserInfo: "/user/",
    sendEmailVerification: "/user/send-email-verification",
    confirmEmail: "/user/confirm-email",
    chatSigns: "/user/chat-signs",
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
    getById: "public/teachers",
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
  studentSchedule: {
    base: "/student/shedules",
  },
  ranks: {
    student: "public/ranks/student",
    teacher: "/public/ranks/teacher",
    course: "/public/ranks/course",
  },
  teacherAbonement: {
    getById: "/teacher/abonements"
  },
  studentAbonement: {
    base: "/student/abonements",
  },
  promocodes: {
    student: "/student/promocodes",
    available: "/student/promocodes/available",
    buy: "/student/promocodes/buy"
  }
}