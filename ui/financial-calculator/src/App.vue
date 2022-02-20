<template>
  <v-app>
    <v-main>
      <Navigation/>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script>
import Navigation from './components/navigation'

export default {
  name: 'app',
  components: {
    Navigation,
  },
}
</script>
