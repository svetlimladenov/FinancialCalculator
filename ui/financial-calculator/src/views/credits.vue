<template>
  <v-main>
    <v-form v-model="valid">
      <v-container>
        <v-row>
          <v-col
            cols="12"
            md="3"
          >
            <v-text-field
              v-model="amount"
              label="Размер на кредита *"
            ></v-text-field>
          </v-col>
          <v-col
            cols="12"
            md="3"
          >
            <v-text-field
              v-model="period"
              label="Срок (месеци)*"
            ></v-text-field>
          </v-col>
          <v-col
            cols="12"
            md="3"
          >
            <v-text-field
              v-model="interest"
              label="Лихва (%) *"
            ></v-text-field>
          </v-col>
          <v-col 
              cols="12"
              md="3"
          >
              <v-btn @click="submit">
                  submit
              </v-btn>
          </v-col>
        </v-row>
      </v-container>
    </v-form>
    <v-data-table
      :headers="headers"
      :items="payments"
      :items-per-page="10"
      class="elevation-1"
    ></v-data-table>
  </v-main>
</template>

<script>
import CalculatorService from '../common/calculator.service'

export default {
  name: "credits-view",
  data: () => ({
      valid: false,
      amount: null,
      period: null,
      interest: null,
  }),
  methods: {
    submit () {
      CalculatorService.calculate({
          amount: this.amount,
          period: this.period,
          interest: this.interest
      }).then((response) => {
        console.log(response);
      })
    },
  }
}
</script>