<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-toolbar class="text-center">
        <q-btn flat dense icon="menu" class="q-ml-xs">
          <q-menu>
            <q-list>
              <q-item clickable v-close-popup to="Scores">
                <q-item-section class="text-dark">Scores</q-item-section>
              </q-item>
              <q-item clickable v-close-popup to="Courses">
                <q-item-section class="text-dark">Courses</q-item-section>
              </q-item>
            </q-list>
          </q-menu>
        </q-btn>
        <q-toolbar-title class="absolute-center">Aaron's Golf Tracker</q-toolbar-title>
      </q-toolbar>
    </q-header>

    <q-page-container>
      <router-view />
    </q-page-container>

    <q-footer elevated class="text-white text-center">
      <q-toolbar>
        <q-toolbar-title>
          <div class="text-subtitle2">
            Created by Aaron Rader &copy;{{ new Date().getFullYear() }}
          </div>
        </q-toolbar-title>
      </q-toolbar>
    </q-footer>
  </q-layout>
</template>
<script>
import { defineComponent } from "vue";
export default defineComponent({
  name: "MainLayout",
  setup() {
    return {};
  },
});
</script>
