<template>
  <div class="text-center">
    <div class="text-h2 text-dark q-mt-md">Scores</div>
    <div class="status q-mt-md text-subtitle2 text-primary">
      {{ state.status }}
    </div>
  </div>

  <q-scroll-area style="height: 55vh">
    <q-card class="q-ml-auto q-mr-auto q-ma-sm text-dark" style="max-width: 70vh">
      <q-list separator>
        <q-item>
          <q-item-section class="text-center text-bold"> Date </q-item-section>
          <q-item-section class="text-center text-bold">
            Player Name
          </q-item-section>
          <q-item-section class="text-center text-bold">
            Course
          </q-item-section>
        </q-item>
        <q-item v-for="score in state.scores" :key="score.id">
          <q-item-section class="text-center">
            {{ formatDate(score.date) }}
          </q-item-section>
          <q-item-section class="text-center">
            {{ score.player }}
          </q-item-section>
          <q-item-section class="text-center">
            {{ score.course.name }} ({{ score.course.section }})
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
      scores: [],
      selectedScoreId: "",
    });

    onMounted(() => {
      loadScores();
    });

    const loadScores = async () => {
      try {
        state.status = "Fetching scores...";

        let response = await fetch(`https://localhost:7063/api/Score`, {
          method: "GET",
        });
        state.scores = await response.json();
        state.status = "";
      } catch (err) {
        console.log(err);
        state.status = "Error fetching scores.";
      }
    };

    const formatDate = (date) => {
      let d;
      // see if date is coming from server
      date === undefined ? (d = new Date()) : (d = new Date(Date.parse(date))); // from server
      let _day = d.getDate();
      let _month = d.getMonth() + 1;
      let _year = d.getFullYear();
      let _hour = d.getHours();
      let _min = d.getMinutes();
      if (_min < 10) {
        _min = "0" + _min;
      }
      return _year + "-" + _month + "-" + _day;
    };

    return { state, loadScores, formatDate };
  },
};
</script>
