
const routes = [
  {
    path: "/",
    component: () => import('layouts/MainLayout.vue'),
    children: [
      {
        path: "/",
        component: () => import('pages/IndexPage.vue')
      },
      {
        path: "/scores",
        name: "Scores",
        component: () => import('pages/ScoresPage.vue')
      },
      {
        path: "/courses",
        name: "Courses",
        component: () => import('pages/CoursesPage.vue')
      }
    ]
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/ErrorNotFound.vue')
  }
]

export default routes
