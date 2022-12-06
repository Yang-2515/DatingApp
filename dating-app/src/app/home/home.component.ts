import { Component, OnInit } from '@angular/core';
import { UserRegister } from '../_models/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  isRegisterMode = false;
  user: UserRegister = { Username: 'giang25', Password: 'giang25', Email: 'giang25@gmail.com'}
  constructor() { }

  ngOnInit(): void {
  }
  cancelRegisterMode(){
    this.isRegisterMode = !this.isRegisterMode;
  }
}
