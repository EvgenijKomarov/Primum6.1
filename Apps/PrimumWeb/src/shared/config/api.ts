export const api = {
  public: {
    login: "/public/login",
    register: "/public/register",
  },
  studentBalance: {
    topup: "/student/balance/request-topup",
    withdrawn: "/student/balance/request-withdrawn"
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
    getLast: "/student/lessons/last",
    getFuture: "/student/lessons/future",
  },
  teacherLesson: {
    getLast: "/teacher/lessons/last",
    getFuture: "/teacher/lessons/future",
    base: "/teacher/lessons"
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
    base: "/teacher/courses",
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