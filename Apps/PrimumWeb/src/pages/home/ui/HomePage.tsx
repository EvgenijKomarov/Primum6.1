import { useState } from 'react';
import { AboutUsBlock } from '../blocks/about-us/AboutUsBlock';
import { CoursesBlock } from '../blocks/courses-block/CoursesBlock';
import { FAQBlock } from '../blocks/faq-block/FAQBlock';
import { GamificationBlock } from '../blocks/gamification-block/GamificationBlock';
import { HowToBeginBlock } from '../blocks/how-to-begin-block/HowToBeginBlock';
import { IntegrationBlock } from '../blocks/integration-block/IntegrationBlock';
import TypewriterText from '../common-elements/TypewriterText/TypewriterText';
import styles from './HomePage.module.css'
import ToggleButton from '@/shared/ui/ToggleButton/ToggleButton';
import { CommonTeacherInfoBlock } from '../blocks/teacher-blocks/common-teacher-info-block/CommonTeacherInfo';
import { GamificationTeacherBlock } from '../blocks/teacher-blocks/gamification-teacher-block/GamificationTeacherBlock';
import { MonetizationTeacherBlock } from '../blocks/teacher-blocks/monetization-teacher-block/MonetizationTeacherBlock';
import { ManagementTeacherBlock } from '../blocks/teacher-blocks/management-teacher-block/ManagementTeacherBlock';
import { IntegrationTeacherBlock } from '../blocks/teacher-blocks/integration-teacher-block/IntegrationTeacherBlock';
import { HowToBeginTeacherBlock } from '../blocks/teacher-blocks/how-to-begin-teacher-block/HowToBeginTeacherBlock';

export const HomePage = () => {
  const [teacherCardsOpen, setTeacherCardsOpen] = useState(false);

  return (
    <div className={styles.homePage}>
      <div className={styles.homeHeader}>
        <div className={styles.headerTitle}>
          <TypewriterText className={styles.title} text='PrimumCode'/>
          <TypewriterText className={styles.subtitle} text='Там, где идеи становятся кодом'/>
        </div>
        <ToggleButton 
          checked={teacherCardsOpen} 
          onChange={setTeacherCardsOpen} 
          label="Для преподавателя"/>
      </div>
      <div className={styles.blocks}>
        {teacherCardsOpen ? 
        <div className={styles.blocks}>
          <div className={styles.blocksColumn}>
            <CommonTeacherInfoBlock/>
            <ManagementTeacherBlock/>
            <IntegrationTeacherBlock/>
          </div>
          <div className={styles.blocksColumn}>
            <GamificationTeacherBlock/>
            <MonetizationTeacherBlock/>
            <HowToBeginTeacherBlock />
          </div>
        </div> :
        <div className={styles.blocks}>
          <div className={styles.blocksColumn}>
            <AboutUsBlock />
            <CoursesBlock />
            <HowToBeginBlock />
          </div>
          <div className={styles.blocksColumn}>
            <IntegrationBlock />
            <GamificationBlock />
            <FAQBlock />
          </div>
        </div>
        }
      </div>
    </div>
  );
}