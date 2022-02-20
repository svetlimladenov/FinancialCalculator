<template>
  <!-- <v-container> -->
    <!-- <div>
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
    </div> -->
      <v-data-table
        :headers="headers"
        :items="payments"
        :items-per-page="10"
        class="elevation-1"
      ></v-data-table>
  <!-- </v-container> -->
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

    headers: [
      { text: 'fwfwf', value: 'date' },
      { text: 'vsve', value: 'monthlyPayment' },
      { text: 'vvdv', value: 'monthlyPrincipal' },
      { text: 'vedve', value: 'monthlyInterest' },
      { text: 'vege', value: 'leftPrincipal' }
    ],
    payments: [
      {
        date: "goshr",
        monthlyPayment: "1",
        monthlyPrincipal: "2",
        monthlyInterest: "3",
        leftPrincipal: "4"
      }
    ],
  }),
  // computed: {
  //   getPayments() {
  //     return this.payments;
  //   }
  // },
  methods: {
    submit () {
      CalculatorService.calculate({
          amount: this.amount,
          period: this.period,
          interest: this.interest
      }).then((response) => {
        this.payments = response.data.message.montlyCreditData;
        console.log(this.payments);
      })
    },
  }
}
</script>