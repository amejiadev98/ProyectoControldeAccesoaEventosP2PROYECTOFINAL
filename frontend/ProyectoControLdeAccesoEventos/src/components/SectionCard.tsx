import type { PropsWithChildren } from 'react'

interface SectionCardProps {
  title: string
  description?: string
}

const SectionCard = ({ title, description, children }: PropsWithChildren<SectionCardProps>) => (
  <section className="section-card">
    <div className="section-card-header">
      <div>
        <h2>{title}</h2>
        {description ? <p>{description}</p> : null}
      </div>
    </div>
    <div className="section-card-body">{children}</div>
  </section>
)

export default SectionCard
