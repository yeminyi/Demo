import { Component, OnInit } from '@angular/core';
import { ScoreService } from '../../services/score.service';
import { ScoreParameters } from '../../models/score-parameters';
import { PageMeta } from '../../../shared/models/page-meta';
import { Score } from '../../models/score';
import { Subject} from 'rxjs'
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
@Component({
  selector: 'app-log-list',
  templateUrl: './log-list.component.html',
  styleUrls: ['./log-list.component.scss']
})
export class LogListComponent  implements OnInit {

  scores: Score[];
  pageMeta: PageMeta;
  scoreParameter = new ScoreParameters({ orderBy: 'id desc', pageSize: 10, pageIndex: 0 });
  searchKeyUp = new Subject<string>();
  constructor(private scoreService: ScoreService) { 
      const subscription = this.searchKeyUp.pipe(
        debounceTime(500),
        distinctUntilChanged()
      ).subscribe(() => {
        this.applyFilter(this.scoreParameter.gameTitle);
      });
    }
  ngOnInit() {
    this.scores = [];
    this.getScores();
    
  }

  getScores() {
    this.scoreService.getList(this.scoreParameter)
    .subscribe(resp => {
      this.pageMeta = JSON.parse(resp.headers.get('X-Pagination')) as PageMeta;
      this.scores = this.scores.concat(resp.body);
   
    });
  }
  onScroll() {
    this.scoreParameter.pageIndex++;
    if (this.scoreParameter.pageIndex < this.pageMeta.pageCount) {
      this.getScores();
    }
  }
  applyFilter(filterValue: string) { 
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.scoreParameter.gameTitle = filterValue;
    this.getScoreswithFilter();
  }

  getScoreswithFilter() {
    this.scoreService.getList(this.scoreParameter).subscribe(resp => {
      this.pageMeta = JSON.parse(resp.headers.get('X-Pagination')) as PageMeta;
      this.scores = resp.body;
    });
  }
}
