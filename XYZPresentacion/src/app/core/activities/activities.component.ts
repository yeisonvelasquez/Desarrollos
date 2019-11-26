import { Component, OnInit } from '@angular/core';
import { ActivitiesService } from './../../services/activities/activities.service';
import { Activities } from './../../interfaces/activities/activities';
import { Router } from '@angular/router';

@Component({
  selector: 'app-activities',
  templateUrl: './activities.component.html',
  styleUrls: ['./activities.component.css']
})
export class ActivitiesComponent implements OnInit {
  public lstActivities: any;
  description: string;

  constructor(private activitiesService: ActivitiesService, private router: Router)
  { }

  ngOnInit() {
    this.activitiesService.GetActivities()
      .subscribe(result => {
        debugger;
        console.log(result);
        if (result.StatusCode == 200){
          this.lstActivities = result.Data;
          this.router.navigate(['activities']);
        }
        else{
          alert(result.Message);
          if (result.StatusCode == 401)
            this.router.navigate(['login']);
        }
      });
  }

  public agregarHoras(idactivity)
  {
    console.log(idactivity);
    this.router.navigate(['timexactivity'], { queryParams: { activityid: idactivity } });
  }

  public registerActivity(){
    const activity= <Activities> {
      Description: this.description
    }
    
    this.activitiesService.RegisterActivity(activity)
      .subscribe(result => {
        debugger;
        console.log(result);
        if (result.StatusCode == 200){
          this.description = '';
          this.lstActivities = result.Data;
          // this.router.navigate(['activities']);
        }
        else
        {
          alert(result.Message);
          if (result.StatusCode == 401)
            this.router.navigate(['login']);
        }
      });
  }

}
