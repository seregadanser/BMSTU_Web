import { Component, ViewChildren, QueryList, OnInit, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from "rxjs";
import { ActivatedRoute, Router, Params } from '@angular/router';
import { InputComponent } from '../../../../input/input.component';
import { ButtonComponent } from '../../../../button/button.component';
import { HeadComponent } from '../../../head/head.component';
import { HradminPageService, MyWorker } from '../../hradmin-page.service';

@Component({
  selector: 'app-add-page',
  standalone: true,
  imports: [CommonModule, InputComponent, ButtonComponent, HeadComponent],
  providers:[HradminPageService],
  templateUrl: './edit-page.component.html',
  styleUrl: './edit-page.component.css'
})
export class EditPageComponent implements AfterViewInit {

  constructor(private route: ActivatedRoute, private router: Router, private HRService: HradminPageService) {  }

  @ViewChildren(InputComponent) children!: QueryList<InputComponent>;

  id : number = 0;

  ngAfterViewInit() {

    this.route.params.subscribe(params=>this.id=params["id"]);

    this.route.queryParams.subscribe( params => {
      console.log("dsd "+params['inputValue1']  );
     // this.children.toArray()[0].inputValue = "ASASA";

      if ('inputValue1' in params)
      this.children.toArray()[0].inputValue = params['inputValue1'];
      if ('inputValue2' in params)
      this.children.toArray()[1].inputValue = params['inputValue2'];
      if ('inputValue3' in params)
      this.children.toArray()[2].inputValue = params['inputValue3'];
      if ('inputValue4' in params)
      this.children.toArray()[3].inputValue = params['inputValue4'];
      if ('inputValue5' in params)
      this.children.toArray()[4].inputValue = params['inputValue5'];
    });
  }

  updateUrl(val: string) {
    const queryParams = {
      inputValue1: this.children.toArray()[0].inputValue,
      inputValue2: this.children.toArray()[1].inputValue,
      inputValue3: this.children.toArray()[2].inputValue,
      inputValue4: this.children.toArray()[3].inputValue,
      inputValue5: this.children.toArray()[4].inputValue,
    };
    this.router.navigate(['hradmin/editUser', this.id], { queryParams });
  }


  EditClick()
  {
    console.log(this.children.toArray()[2].inputValue);
    this.HRService.editUser(this.id,new MyWorker(
      this.id,
      this.children.toArray()[0].inputValue,
      this.children.toArray()[1].inputValue,
      this.children.toArray()[2].inputValue,
      this.children.toArray()[3].inputValue,
      this.children.toArray()[4].inputValue, 0 )


     ).subscribe(
      (error) => {
  console.error('Error fetching users:', error);
}
);
     this.router.navigate(['hradmin']);
  }

  ExitClick()
  {
  this.router.navigate(['hradmin']);
  }
}
