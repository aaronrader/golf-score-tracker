<template>
  <div class="text-center">
    <div class="text-h2 text-dark q-mt-md">Courses</div>
    <div class="status q-mt-md text-subtitle2 text-primary">
      {{ state.status }}
    </div>
  </div>

  <q-scroll-area style="height: 55vh">
    <q-card class="q-ml-auto q-mr-auto q-ma-sm text-dark" style="max-width: 70vh">
      <q-list separator>
        <q-item>
          <q-item-section class="text-center text-bold"> Course </q-item-section>
          <q-item-section class="text-center text-bold">
            Section
          </q-item-section>
        </q-item>
        <q-item v-for="course in state.courses" :key="course.id">
          <q-item-section class="text-center">
            {{ course.name }}
          </q-item-section>
          <q-item-section class="text-center">
            {{ course.section }}
          </q-item-section>
        </q-item>
      </q-list>
    </q-card>
  </q-scroll-area>
</template>

<script>
//import { useQuasar } from "quasar";
import { reactive, onMounted } from "vue";

export default {
  setup() {
    // sets dark mode
    // const $q = useQuasar();
    // $q.dark.set(true);

    let state = reactive({
      status: "",
      courses: [],
      selectedCourseId: "",
    });

    onMounted(() => {
      loadCourses();
    });

    const loadCourses = async () => {
      try {
        state.status = "Fetching courses...";

        let response = await fetch(`https://localhost:7063/api/course`, {
          method: "GET",
        });
        state.courses = await response.json();
        state.status = "";
      } catch (err) {
        console.log(err);
        state.status = "Error fetching courses.";
      }
    };

    return { state, loadCourses };
  },
};
</script>
