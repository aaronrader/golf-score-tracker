<template>
  <div class="text-center text-dark">
    <div class="text-h2 text-dark q-mt-md">Courses</div>
    <div class="status q-mt-md text-subtitle2 text-primary">
      {{ state.status }}
    </div>

    <q-select
    class="q-ml-auto q-mr-auto"
    v-if="state.courses.length > 0"
    style="max-width: 20vw;"
    :option-value="'id'"
    :option-label="'name'"
    :options="state.courses"
    label="Select a Course"
    v-model="state.selectedCourseId"
    @update:model-value="getHoles()"
    emit-value
    map-options
    />

    <q-scroll-area
    class="q-ml-auto q-mr-auto q-mt-md"
    v-if="state.holes.length > 0"
    style="height: 70vh;
    max-width: 20vw;">
      <q-card class="q-ma-sm">
        <q-list dense separator>
          <q-item>
            <q-item-section class="text-center">
              Hole #
            </q-item-section>
            <q-item-section class="text-center">
              Par
            </q-item-section>
            <q-item-section class="text-center">
              Distance (yds)
            </q-item-section>
          </q-item>
          <q-item
            v-for="hole in state.holes"
            :key="hole.id"
          >
            <q-item-section class="text-center">
              {{ hole.holeNum }}
            </q-item-section>
            <q-item-section class="text-center">
              {{ hole.par }}
            </q-item-section>
            <q-item-section class="text-center">
              {{ hole.distance }}
            </q-item-section>
          </q-item>
        </q-list>
      </q-card>
    </q-scroll-area>

  </div>

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
      holes: [],
      selectedCourseId: "",
      selectedCourse: {},
    });

    onMounted(() => {
      loadCourses();
    });

    const loadCourses = async () => {
      try {
        state.status = "Fetching courses...";

        let response = await fetch(`https://localhost:7063/api/course`, { method: "GET" });
        state.courses = await response.json();
        state.status = "";
      } catch (err) {
        console.log(err);
        state.status = "Error fetching courses.";
      }
    };

    const getHoles = async () => {
      try {
        state.selectedCourse = state.courses.find(
          (course) => course.id === state.selectedCourseId
        );

        state.status = "Fetching holes...";

        let response = await fetch(`https://localhost:7063/api/hole/${state.selectedCourse.id}`, { method: "GET" });
        state.holes = await response.json();
        state.status = ``;
      } catch (err) {
        console.log(err);
        state.status = "Error fetching courses.";
      }
    };

    return { state, loadCourses, getHoles };
  },
};
</script>
