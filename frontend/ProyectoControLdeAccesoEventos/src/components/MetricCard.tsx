interface MetricCardProps {
  value: string
  label: string
}

const MetricCard = ({ value, label }: MetricCardProps) => (
  <div className="metric-card">
    <span className="metric-value">{value}</span>
    <span className="metric-label">{label}</span>
  </div>
)

export default MetricCard
