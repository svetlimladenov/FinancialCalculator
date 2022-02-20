<template>
    <v-bottom-navigation>
        <v-btn v-for="item in menuItems" :key="item.title" :to="item.path">
            {{ item.title }}
        </v-btn>
    </v-bottom-navigation>
</template>

<script>
export default {
    name: "navigation-bar",
    data() {
        return {
            appTitle: "Calculator",
            menuItems: [
                {
                    title: "Home",
                    path: "/"
                },
                {
                    title: "Credits",
                    name: "credits-view",
                    path: "/credits"
                }
            ]
        }
    }
}
</script>
