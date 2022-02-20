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
    <v-table v-show="payments.length">
      <template v-slot:default>
        <thead>
          <tr>
            <th class="text-left">
              Дата
            </th>
            <th class="text-left">
              Месечна вноска
            </th>
            <th class="text-left">
              Вноска главница
            </th>
            <th class="text-left">
              Вноска лихва
            </th>
            <th class="text-left">
              Остатък главница
            </th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="item in payments"
            :key="item.date"
          >
            <td>{{ item.date }}</td>
            <td>{{ item.monthlyPayment }}</td>
            <td>{{ item.monthlyPrincipal }}</td>
            <td>{{ item.monthlyInterest }}</td>
            <td>{{ item.leftPrincipal }}</td>
          </tr>
        </tbody>
      </template>
    </v-table>
  </v-main>
</template>

<script>
  import CalculatorService from '../common/calculator.service'

  export default {
    name: 'lease-view',
    data () {
      return {
        valid: false,
        amount: null,
        period: null,
        interest: null,

        payments: [],
      }
    },
    methods: {
      submit () {
        CalculatorService.credit({
            amount: this.amount,
            period: this.period,
            interest: this.interest
        }).then((response) => {
          this.payments = response.data.message.montlyCreditData;
        });
      }
    }
  }
</script>