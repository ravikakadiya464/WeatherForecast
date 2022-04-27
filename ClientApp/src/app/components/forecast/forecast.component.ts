import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { WeatherForecast } from 'src/app/models/WeatherForecast';
import { IWeatherForecast } from 'src/app/models/IWeatherForecast';

@Component({
  selector: 'app-forecast',
  templateUrl: './forecast.component.html',
  styleUrls: ['./forecast.component.scss']
})
export class ForecastComponent implements OnInit {

  @Input() forecast: IWeatherForecast;
  @Input() index: number;
  @Input() active: boolean;
  @Input() background: string;
  @Output() onDayClick: EventEmitter<number>;

  constructor() {
    this.forecast = new WeatherForecast();
    this.onDayClick = new EventEmitter();
    this.index = 0;
    this.active = false;
    this.background = '';
  }

  ngOnInit(): void {
  }

  setForecast(): void {
    this.onDayClick.emit(this.index);
  }
}
