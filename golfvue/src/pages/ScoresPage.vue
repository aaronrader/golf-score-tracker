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
          <q-item-section class="text-center text-bold"
          clickable
          @click="sortScores('Date')"
          > Date </q-item-section>
          <q-item-section class="text-center text-bold"
          clickable
          @click="sortScores('Player')"
          > Player Name
          </q-item-section>
          <q-item-section class="text-center text-bold"
          clickable
          @click="sortScores('Course')"
          > Course
          </q-item-section>
          <q-item-section class="text-center text-bold">
            Notes
          </q-item-section>
          <q-item-section class="text-center text-bold"
          clickable
          @click="sortScores('Score')"
          > Score
          </q-item-section>
        </q-item>
        <q-item
        v-for="score in state.scores"
        :key="score.id"
        clickable
        @click="selectScore(score.id)"
        >
          <q-item-section class="text-center">
            {{ formatDate(score.date) }}
          </q-item-section>
          <q-item-section class="text-center">
            {{ score.player }}
          </q-item-section>
          <q-item-section class="text-center">
            {{ score.course.name }}
          </q-item-section>
          <q-item-section class="text-center">
            {{ score.notes }}
          </q-item-section>
          <q-item-section class="text-center">
            {{ formatNumber(score.totalScore) }}
          </q-item-section>
        </q-item>
      </q-list>
    </q-card>
  </q-scroll-area>


  <q-dialog v-model="state.scoreSelected">
  <q-card style="max-width: 100vw; width:fit-content">
    <q-card-actions align="right">
        <q-btn flat label="X" color="primary" v-close-popup class="text-h5" />
      </q-card-actions>
      <q-card-section>
        <div class="text-h5 text-center text-primary text-bold">
          {{ state.selectedScore.player }} - {{ state.selectedScore.course.name }}
        </div>
        <div class="text-h5 text-center text-primary">
          {{ formatDate(state.selectedScore.date) }}
        </div>
      </q-card-section>
      <q-card-section>
        <q-list separator>
          <!--Hole Number Section-->
          <q-item>
            <q-item-section style="flex: 0 0 55px;">Hole</q-item-section>
            <q-item-section
            class="q-pa-sm text-right"
            style="flex: 0 0 30px;"
            v-for="hole in state.scoreHoles"
            :key="hole.id"
            >
              {{ hole.hole.holeNum }}
            </q-item-section>
            <q-item-section class="text-right text-bold" style="flex: 0 0 40px;">Total</q-item-section>
          </q-item>

          <!--Par Section-->
          <q-item>
            <q-item-section style="flex: 0 0 55px;">Par</q-item-section>
            <q-item-section
            class="q-pa-sm text-right"
            style="flex: 0 0 30px;"
            v-for="hole in state.scoreHoles"
            :key="hole.id"
            >
              {{ hole.hole.par }}
            </q-item-section>
            <q-item-section class="text-right text-bold" style="flex: 0 0 40px;">{{ state.totalPar }}</q-item-section>
          </q-item>

          <!--Strokes Section-->
          <q-item>
            <q-item-section style="flex: 0 0 55px;">Strokes</q-item-section>
            <q-item-section
            class="q-pa-sm text-right"
            style="flex: 0 0 30px;"
            v-for="hole in state.scoreHoles"
            :key="hole.id"
            >
              {{ hole.strokes }}
            </q-item-section>
            <q-item-section class="text-right text-bold" style="flex: 0 0 40px;">{{ state.totalStrokes }}</q-item-section>
          </q-item>

          <!--Score Section-->
          <q-item>
            <q-item-section style="flex: 0 0 55px;">Score</q-item-section>
            <q-item-section
            class="q-pa-sm text-right"
            style="flex: 0 0 30px;"
            v-for="hole in state.scoreHoles"
            :key="hole.id"
            >
              {{ formatNumber(hole.strokes - hole.hole.par) }}
            </q-item-section>
            <q-item-section class="text-right text-bold" style="flex: 0 0 40px;">{{ formatNumber(state.totalScore) }}</q-item-section>
          </q-item>
        </q-list>
      </q-card-section>
  </q-card>

  </q-dialog>
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
      scoreHoles: [],
      selectedScore: {},
      selectedScoreId: "",
      scoreSelected: false,
      totalPar: 0,
      totalStrokes: 0,
      totalScore: 0,
    });

    let dateOrder = false;
    let playerOrder = false;
    let courseOrder = false;
    let scoreOrder = false;

    onMounted(() => {
      loadScores();
    });

    const loadScores = async () => {
      try {
        state.status = "Fetching scores...";

        let response = await fetch(`https://localhost:7063/api/Score`, { method: "GET" });
        state.scores = await response.json();
        state.status = "";
      } catch (err) {
        console.log(err);
        state.status = "Error fetching scores.";
      }
    };

    const selectScore = async (scoreId) => {
      try {
        state.selectedScore = state.scores.find(
          (score) => score.id === scoreId
        );

        state.status = "Fetching score data...";

        let response = await fetch(`https://localhost:7063/api/ScoreHole/${state.selectedScore.id}`)
        state.scoreHoles = await response.json();

        state.totalPar = 0;
        state.totalStrokes = 0;
        state.totalScore = 0;

        state.scoreHoles.forEach(
          (hole) => {
            state.totalPar += hole.hole.par;
            state.totalStrokes += hole.strokes;
          }
        );
        state.totalScore = state.selectedScore.totalScore;

        state.scoreSelected = true;
        state.status = "";
      } catch (err) {
        console.log(err);
        state.status = "Error fetching scores.";
      }
    };

    const sortScores = (sortMethod) => {
      switch (sortMethod) {
        case "Date":
          if (dateOrder)
            dateOrder = false;
          else
            dateOrder = true;

          state.scores = state.scores.sort(
            (a, b) => {
              if (dateOrder) {
                return Date.parse(b.date) - Date.parse(a.date);
              }
              else {
                return Date.parse(a.date) - Date.parse(b.date);
              }
            }
          );
        break;
        case "Player":
          if (playerOrder)
            playerOrder = false;
          else
            playerOrder = true;

          state.scores = state.scores.sort(
            (a, b) => {
              if (playerOrder) {
                return b.player - a.player;
              }
              else {
                return a.player - b.player;
              }
            }
          );
        break;
        case "Course":
          if (courseOrder)
            courseOrder = false;
          else
            courseOrder = true;

          state.scores = state.scores.sort(
            (a, b) => {
              if (courseOrder) {
                return b.course.name - a.course.name;
              }
              else {
                return a.course.name - b.course.name;
              }
            }
          );
        break;
        case "Score":
          if (scoreOrder)
          scoreOrder = false;
          else
          scoreOrder = true;

          state.scores = state.scores.sort(
            (a, b) => {
              if (scoreOrder) {
                return b.totalScore - a.totalScore;
              }
              else {
                return a.totalScore - b.totalScore;
              }
            }
          );
        break;
      };
    };

    const formatDate = (date) => {
      let d;
      // see if date is coming from server
      date === undefined ? (d = new Date()) : (d = new Date(Date.parse(date))); // from server
      let _day = d.getDate();
      let _month = d.getMonth() + 1;
      if (_month < 10) {
        _month = "0" + _month;
      }
      let _year = d.getFullYear();
      return _year + "-" + _month + "-" + _day;
    };

    const formatNumber = (number) => {
      if (number > 0)
        return "+" + number;
      if (number < 0)
        return "-" + number;

        return number;
    };

    return { state, loadScores, selectScore, sortScores, formatNumber, formatDate };
  },
};
</script>
