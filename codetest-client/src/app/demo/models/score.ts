import { Entity } from '../../shared/models/entity';

export class Score extends Entity {
    gameTitle: string;
    teamA: string;
    teamB: string;
    teamAScore: Number;
    teamBScore: Number;
    employee: string;
    updateTime: Date;
    
}
