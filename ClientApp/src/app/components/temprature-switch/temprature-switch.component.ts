import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-temprature-switch',
  templateUrl: './temprature-switch.component.html',
  styleUrls: ['./temprature-switch.component.scss']
})
export class TempratureSwitchComponent implements OnInit {
  @Output() onUnitChange: EventEmitter<boolean>;
  isCelsius: boolean;
  constructor() {
    this.isCelsius = true;
    this.onUnitChange = new EventEmitter<boolean>();
  }

  ngOnInit(): void {
  }

  changeUnit(): void {
    this.onUnitChange.emit(this.isCelsius);
  }

}
