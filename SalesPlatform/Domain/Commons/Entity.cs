using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesPlatform.Domain.Commons
{
    /// <summary>
    /// Entity base class.
    /// </summary>
    public abstract class Entity
    {
        private int? requestedHashCode;
        private Guid id;

        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public virtual Guid Id
        {
            get
            {
                return id;
            }

            protected set
            {
                id = value;
            }
        }

        /// <summary>
        /// Gets comparable object.
        /// </summary>
        /// <param name="left">Entity object left.</param>
        /// <param name="right">Entity object right.</param>
        /// <returns>Returns true o false.</returns>
        public static bool operator ==(Entity left, Entity right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }
            else
            {
                return left.Equals(right);
            }
        }

        /// <summary>
        /// Gets comparable object.
        /// </summary>
        /// <param name="left">Entity object left.</param>
        /// <param name="right">Entity object right.</param>
        /// <returns>Returns true o false.</returns>
        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Is transient.
        /// </summary>
        /// <returns>Returns true o false.</returns>
        public bool IsTransient()
        {
            return Id == default;
        }

        /// <summary>
        /// Gets comparable object.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>Returns true o false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            Entity item = (Entity)obj;

            if (item.IsTransient() || IsTransient())
            {
                return false;
            }
            else
            {
                return item.Id == Id;
            }
        }

        /// <summary>
        /// Get HashCode.
        /// </summary>
        /// <returns>Returns int value.</returns>
        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!requestedHashCode.HasValue)
                {
                    requestedHashCode = Id.GetHashCode() ^ 31;
                }

                return requestedHashCode.Value;
            }
            else
            {
                return base.GetHashCode();
            }
        }
    }
}
